import { createSlice, createAsyncThunk, PayloadAction } from "@reduxjs/toolkit";
import { ApiError } from "../../api/constants/apiError";
import { ApiStatus, apiStatus } from "../../api/constants/apiStatus";
import {
  Profile,
  ProfilesApiResponse,
} from "../../api/constants/profilesApiResponse";
import { fetchUserProfiles } from "../../api/profilesApi";

interface UserProfiles {
  error: ApiError | null;
  status: ApiStatus;
  profiles: Profile[];
  profilesPage: number;
  profilesPageSize: number;
  profilesLength: number;
  profilesPosition: number;
  totalPages: number;
}

const initialState: UserProfiles = {
  error: null,
  status: apiStatus.IDLE,
  totalPages: NaN,
  profilesPosition: 0,
  profilesPage: 1,
  profilesPageSize: 5,
  profilesLength: 0,
  profiles: [],
};

export const fetchProfiles = createAsyncThunk(
  "profiles/fetchUserProfiles",
  async () => {
    return await fetchUserProfiles();
  }
);

const profilesSlice = createSlice({
  name: "profiles",
  initialState: initialState,
  reducers: {
    setProfiles(state, action: PayloadAction<Profile[]>) {
      state.profiles = action.payload;
      state.profilesLength = action.payload.length;
    },
    setProfilesPosition(state, action: PayloadAction<number>) {
      state.profilesPosition = action.payload;
    },
    setProfilesPage(state, action: PayloadAction<number>) {
      state.profilesPage = action.payload;
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
        (state, action: PayloadAction<ProfilesApiResponse>) => {
          state.status = apiStatus.SUCCESS;
          state.profiles = action.payload.profilesList;
          state.profilesLength = action.payload.filterPageData.pageSize;
          state.totalPages = action.payload.filterPageData.numPages;
        }
      )
      .addCase(fetchProfiles.rejected, (state) => {
        state.status = apiStatus.ERROR;
      });
  },
});

export const { setProfiles, setProfilesPosition, setProfilesPage, setStatus } =
  profilesSlice.actions;
export default profilesSlice.reducer;
