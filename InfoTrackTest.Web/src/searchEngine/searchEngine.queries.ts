import { FETCH_QUERIES_NAME } from "../core/constants";
import { useQuery } from "@tanstack/react-query";
import { getSearchEngineOptionsAsync } from "./searchEngine.services";

export const useFetchSearchEngineOptions = () => {
  return useQuery(
    [FETCH_QUERIES_NAME.SEARCH_ENGINE_OPTIONS],
    async () => {
      const result = await getSearchEngineOptionsAsync();
      return result;
    },
    {
      refetchOnWindowFocus: false,
    }
  );
};
