import { IPaginagedResult } from "../core/IPaginatedResult";
import { AXIOS_API_SERVER } from "../config/config.constants";
import { envConfigs } from "../config/EnvConfigs";
import { getAxiosApiInstanceByName } from "../core/axios/axios.services";
import { IHistory, IHistoryDashboard } from "./history.models";

export const apiRoutes = {
  getHistories: "/Histories",
  getHistoryDashboard: "/Histories/Dashboard",
};

export interface IGetHistoriesProps {
  page: number;
  pageSize: number;
}

export const getPaginagedHistoriesAsync = async (
  props: IGetHistoriesProps
): Promise<IPaginagedResult<IHistory>> => {
  const result = await getAxiosApiInstanceByName(AXIOS_API_SERVER).get<
    IPaginagedResult<IHistory>
  >(`${envConfigs.apiBaseUrl}${apiRoutes.getHistories}`, { params: props });
  return result.data;
};

export const getHistoryDashboardAsync = async (): Promise<
  IHistoryDashboard[]
> => {
  const result = await getAxiosApiInstanceByName(AXIOS_API_SERVER).get<
    IHistoryDashboard[]
  >(`${envConfigs.apiBaseUrl}${apiRoutes.getHistoryDashboard}`);
  return result.data;
};
