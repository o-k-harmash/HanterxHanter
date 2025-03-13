import { useMemo } from "react";
import { defaultApiStatuses, ApiStatus } from "../api/constants/apiStatus";
import { capitalize } from "../utils/pipes";
import { useAppSelector } from "../app/hooks";
import { RootState } from "../app/store";

type Statuses = Record<`is${Capitalize<Lowercase<ApiStatus>>}`, boolean>;

const prepareStatuses = (currentStatus: ApiStatus): Statuses => {
  const statuses = {} as Statuses;
  for (const status of defaultApiStatuses) {
    const normalisedStatus = capitalize(status.toLowerCase());
    const normalisedStatusKey = `is${normalisedStatus}` as keyof Statuses;
    statuses[normalisedStatusKey] = status === currentStatus;
  }
  return statuses;
};

export const useApiStatus = (
  statusSelector: (state: RootState) => ApiStatus
) => {
  const status = useAppSelector(statusSelector);
  const statuses = useMemo(() => prepareStatuses(status), [status]);
  return {
    status,
    ...statuses,
  };
};
