import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import { apiStatus, ApiStatus } from "../../api/constants/apiStatus";
import { ApiError } from "../../api/constants/apiError";
import { getUser, User } from "../../api/api";

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
  return await getUser("a0e08192-e158-48fc-9a02-e5a833c1c4af");
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
