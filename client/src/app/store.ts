import type { Action, ThunkAction } from "@reduxjs/toolkit";
import { configureStore } from "@reduxjs/toolkit";
import profilesReducer from "../features/profiles/profilesSlice";
import accountReducer from "../features/account/accountSlice";

export const store = configureStore({
  reducer: {
    profiles: profilesReducer,
    account: accountReducer,
  },
});

export type AppStore = typeof store;
export type RootState = ReturnType<AppStore["getState"]>;
export type AppDispatch = AppStore["dispatch"];
export type AppThunk<ThunkReturnType = void> = ThunkAction<
  ThunkReturnType,
  RootState,
  unknown,
  Action
>;
