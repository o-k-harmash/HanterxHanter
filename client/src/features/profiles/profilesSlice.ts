import { createSlice, createAsyncThunk, PayloadAction } from "@reduxjs/toolkit";
import { ApiError } from "../../api/constants/apiError";
import { ApiStatus, apiStatus } from "../../api/constants/apiStatus";
import { fetchProfileList, ProfileList } from "../../api/api";

interface UserProfiles {
  error: ApiError | null;
  status: ApiStatus;
  profileList: ProfileList | null;
  profilesPosition: number;
}

const initialState: UserProfiles = {
  error: null,
  status: apiStatus.IDLE,
  profilesPosition: 1,
  profileList: null,
};

export const fetchProfiles = createAsyncThunk(
  "profiles/fetchUserProfiles",
  async () => {
    return await fetchProfileList({
      genderId: "male",
      cityId: "new_york",
      minAge: "0",
      maxAge: "30",
      pageNum: "1",
      pageSize: "10",
    });
  }
);

const profilesSlice = createSlice({
  name: "profiles",
  initialState: initialState,
  reducers: {
    setProfiles(state, action: PayloadAction<ProfileList>) {
      state.profileList = action.payload;
    },
    setProfilesPosition(state, action: PayloadAction<number>) {
      state.profilesPosition = action.payload;
    },
    setStatus(state, action: PayloadAction<ApiStatus>) {
      state.status = action.payload;
    },
  },
  extraReducers: (builder) => {
    builder
      .addCase(fetchProfiles.pending, (state) => {
        state.status = apiStatus.PENDING;
      })
      .addCase(
        fetchProfiles.fulfilled,
        (state, action: PayloadAction<ProfileList>) => {
          state.status = apiStatus.SUCCESS;
          state.profileList = action.payload;
        }
      )
      .addCase(fetchProfiles.rejected, (state) => {
        state.status = apiStatus.ERROR;
      });
  },
});

export const { setProfiles, setProfilesPosition, setStatus } =
  profilesSlice.actions;
export default profilesSlice.reducer;
