import { FETCH_QUERIES_NAME } from "../core/constants";
import { useQuery } from "@tanstack/react-query";
import { isPositiveNumber } from "../core/number.services";
import { searchAsync } from "./search.services";

export const useFetchSearchResult = (
  searchEngineId: number,
  keywords: string
) => {
  return useQuery(
    [searchEngineId, FETCH_QUERIES_NAME.SEARCH, keywords],
    async () => {
      const result = await searchAsync({ searchEngineId, keywords });
      return result;
    },
    {
      enabled:
        isPositiveNumber(searchEngineId) && !!keywords && keywords !== "",
      refetchOnWindowFocus: false,
    }
  );
};
