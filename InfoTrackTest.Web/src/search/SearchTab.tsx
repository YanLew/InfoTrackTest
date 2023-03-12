import * as React from "react";
import { ISearchEngineOption } from "../searchEngine/searchEngine.models";
import { useFetchSearchEngineOptions } from "../searchEngine/searchEngine.queries";
import { useFetchSearchResult } from "./search.queries";
import { TextField } from "../core/TextField";
import Container from "react-bootstrap/Container";
import Row from "react-bootstrap/Row";
import Col from "react-bootstrap/Col";
import { Select } from "../core/Select";
import { BaseContainer } from "../core/BaseContainer";
import Alert from "react-bootstrap/Alert";
import Spinner from "react-bootstrap/Spinner";
import "./SearchTab.scss";
import { FETCH_QUERIES_NAME } from "../core/constants";
import { useQueryClient } from "@tanstack/react-query";

interface ISearchTabProps {}

export const SearchTab: React.FunctionComponent<ISearchTabProps> = ({}) => {
  const queryClient = useQueryClient();
  const [searchKeywords, setSearchKeywords] = React.useState<string>("");
  const [searchEngineId, setSearchEngineId] = React.useState<
    number | undefined
  >(undefined);

  const { data: searchEngineOptions, isLoading: isLoadingSearchEngineOptions } =
    useFetchSearchEngineOptions();

  var defaultSearchEngineId = React.useMemo(() => {
    return searchEngineOptions
      ? searchEngineOptions.find((o) => o.name === "Google")?.id.toString() ||
          undefined
      : undefined;
  }, [JSON.stringify(searchEngineOptions)]);

  React.useEffect(() => {
    setSearchEngineId(Number(defaultSearchEngineId) || undefined);
  }, [defaultSearchEngineId]);

  const {
    data: searchResultRanks,
    isLoading: isLoadingSearchResultRanks,
    error,
    isSuccess,
  } = useFetchSearchResult(searchEngineId || 0, searchKeywords);

  React.useEffect(() => {
    if (isSuccess) {
      queryClient.invalidateQueries([FETCH_QUERIES_NAME.HISTORIES]);
      queryClient.invalidateQueries([FETCH_QUERIES_NAME.HISTORY_DASHBOARD]);
    }
  }, [isSuccess]);

  return (
    <BaseContainer>
      <Container>
        <Row>
          <Col>
            <TextField
              shouldDebounce
              placeholder="Enter keywords for searching"
              value={searchKeywords}
              onChange={(newValue) => {
                setSearchKeywords(newValue ?? "");
              }}
            />
          </Col>
          <Col xs="3">
            <Select<ISearchEngineOption>
              options={searchEngineOptions || []}
              idFieldName="id"
              displayFieldName="name"
              onSelected={(id: string | undefined) => {
                setSearchEngineId(id ? Number(id) : undefined);
              }}
              defalutOption={defaultSearchEngineId}
            />
          </Col>
        </Row>
        {!!isLoadingSearchResultRanks &&
          !!searchEngineId &&
          !!searchKeywords &&
          searchKeywords != "" && (
            <Row className="info-tract-test-search-tab-result">
              <Col>
                <div className="info-tract-test-search-tab-loading">
                  <Spinner animation="border" role="status">
                    <span className="visually-hidden">Loading...</span>
                  </Spinner>
                </div>
              </Col>
            </Row>
          )}
        {!!searchResultRanks && (
          <Row className="info-tract-test-search-tab-result">
            <Col>
              <Alert variant="primary">
                {searchResultRanks === "0"
                  ? "InfoTrack site is not in the top 100 search result."
                  : `InfoTrack site is in the top 100 search result. Rank(s): ${searchResultRanks}`}
              </Alert>
            </Col>
          </Row>
        )}
        {!!error && (
          <Row className="info-tract-test-search-tab-result">
            <Col>
              <Alert variant="danger">
                Error occurs while searching, please be noted that error 429
                sometimes occurs while sending too many request. Please try
                again later, or use another search engine.
              </Alert>
            </Col>
          </Row>
        )}
      </Container>
    </BaseContainer>
  );
};
