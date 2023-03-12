import { FETCH_QUERIES_NAME } from "../core/constants";
import { useQuery } from "@tanstack/react-query";
import { isPositiveNumber } from "../core/number.services";
import {
  getHistoryDashboardAsync,
  getPaginagedHistoriesAsync,
} from "./history.services";

export const useFetchPaginatedHistories = (page: number, pageSize: number) => {
  return useQuery(
    [FETCH_QUERIES_NAME.HISTORIES, page, pageSize],
    async () => {
      const result = await getPaginagedHistoriesAsync({ page, pageSize });
      return result;
    },
    {
      enabled: isPositiveNumber(page) && isPositiveNumber(pageSize),
      refetchOnWindowFocus: true,
    }
  );
};

export const useFetchHistoryDashboard = () => {
  return useQuery(
    [FETCH_QUERIES_NAME.HISTORY_DASHBOARD],
    async () => {
      const result = await getHistoryDashboardAsync();
      return result;
    },
    {
      enabled: true,
      refetchOnWindowFocus: true,
    }
  );
};
