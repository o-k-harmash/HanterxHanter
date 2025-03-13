import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import { apiStatus, ApiStatus } from "../../api/constants/apiStatus";
import { ApiError } from "../../api/constants/apiError";
import { User } from "../../api/constants/usersApiResponse";
import { fetchUser } from "../../api/usersApi";

interface Account {
  status: ApiStatus;
  error: ApiError;
  account: User | null;
}

const initialState: Account = {
  status: apiStatus.IDLE,
  error: null,
  account: null,
};

export const getAccount = createAsyncThunk("account/getAccount", async () => {
  return await fetchUser();
});

const accountSlice = createSlice({
  name: "account",
  initialState: initialState,
  reducers: {},
  extraReducers: (builder) => {
    builder
      .addCase(getAccount.pending, (state) => {
        state.status = apiStatus.PENDING;
      })
      .addCase(getAccount.fulfilled, (state, action) => {
        state.status = apiStatus.SUCCESS;
        state.account = action.payload;
      })
      .addCase(getAccount.rejected, (state) => {
        state.status = apiStatus.ERROR;
      });
  },
});

export default accountSlice.reducer;
