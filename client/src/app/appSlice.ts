import { createSlice, createAsyncThunk, PayloadAction } from "@reduxjs/toolkit";
import { ApiError } from "../api/constants/apiError";
import { apiStatus, ApiStatus } from "../api/constants/apiStatus";
import { DropdownList, fetchDropdownList } from "../api/api";

interface App {
  error: ApiError | null;
  status: ApiStatus;
  dropdownList: DropdownList | null;
  locale: "ru" | "en";
}

const initialState: App = {
  error: null,
  status: apiStatus.IDLE,
  dropdownList: null,
  locale: "ru",
};

export const fetchDropdown = createAsyncThunk(
  "dropdown/fetchDropdownList",
  async () => {
    return await fetchDropdownList();
  }
);

const appSlice = createSlice({
  name: "app",
  initialState: initialState,
  reducers: {
    setDropdownList(state, action: PayloadAction<DropdownList>) {
      state.dropdownList = action.payload;
    },
    setStatus(state, action: PayloadAction<ApiStatus>) {
      state.status = action.payload;
    },
  },
  extraReducers: (builder) => {
    builder
      .addCase(fetchDropdown.pending, (state) => {
        state.status = apiStatus.PENDING;
      })
      .addCase(
        fetchDropdown.fulfilled,
        (state, action: PayloadAction<DropdownList>) => {
          state.status = apiStatus.SUCCESS;
          state.dropdownList = action.payload;
        }
      )
      .addCase(fetchDropdown.rejected, (state) => {
        state.status = apiStatus.ERROR;
      });
  },
});

export const { setDropdownList, setStatus } = appSlice.actions;
export default appSlice.reducer;
