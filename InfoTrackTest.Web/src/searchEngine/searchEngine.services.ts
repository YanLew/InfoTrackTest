import { AXIOS_API_SERVER } from "../config/config.constants";
import { envConfigs } from "../config/EnvConfigs";
import { getAxiosApiInstanceByName } from "../core/axios/axios.services";
import { ISearchEngineOption } from "./searchEngine.models";

export const apiRoutes = {
  searchEngineOptionsRequest: "/SearchEngines/Options",
};

export const getSearchEngineOptionsAsync = async (): Promise<
  ISearchEngineOption[]
> => {
  const result = await getAxiosApiInstanceByName(AXIOS_API_SERVER).get<
    ISearchEngineOption[]
  >(`${envConfigs.apiBaseUrl}${apiRoutes.searchEngineOptionsRequest}`);
  return result.data;
};
