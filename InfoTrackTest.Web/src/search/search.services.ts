import { AXIOS_API_SERVER } from "../config/config.constants";
import { envConfigs } from "../config/EnvConfigs";
import { getAxiosApiInstanceByName } from "../core/axios/axios.services";

export const apiRoutes = {
  searchRequest: "/Searches",
};

export interface ISearchProps {
  searchEngineId: number;
  keywords: string;
}

export const searchAsync = async (props: ISearchProps): Promise<string> => {
  const result = await getAxiosApiInstanceByName(AXIOS_API_SERVER).get<string>(
    `${envConfigs.apiBaseUrl}${apiRoutes.searchRequest}`,
    { params: props }
  );
  return result.data;
};
