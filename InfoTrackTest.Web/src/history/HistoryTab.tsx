import * as React from "react";
import Table from "react-bootstrap/Table";
import { BaseContainer } from "../core/BaseContainer";
import "./HistoryTab.scss";
import { useFetchPaginatedHistories } from "./history.queries";
import { formatIsoDateTimeString } from "../core/dateTime.services";
import { Paginator } from "../core/Paginator";

interface IHistoryTabProps {}

export const HistoryTab: React.FunctionComponent<IHistoryTabProps> = () => {
  const [page, setPage] = React.useState<number>(1);
  const pageSize = 10;

  const { data: paginatedHistoriesResult, isLoading } =
    useFetchPaginatedHistories(page, pageSize);

  return (
    <BaseContainer>
      <Table striped bordered hover>
        <thead>
          <tr>
            <th>#</th>
            <th>Search Engine</th>
            <th>Keywords</th>
            <th>Ranks</th>
            <th>Date</th>
          </tr>
        </thead>
        <tbody>
          {(paginatedHistoriesResult?.items || []).map((history) => {
            return (
              <tr key={history.id.toString()}>
                <td>{history.id}</td>
                <td>{history.searchEngineName}</td>
                <td>{history.keyword}</td>
                <td>{history.rank}</td>
                <td>{formatIsoDateTimeString(history.createdDateTime)}</td>
              </tr>
            );
          })}
        </tbody>
      </Table>
      <Paginator
        maxPageNumber={Math.ceil(
          (paginatedHistoriesResult?.totalCount || 1) / pageSize
        )}
        currentPage={page}
        onPageChanged={(newPage) => {
          setPage(newPage);
        }}
      />
    </BaseContainer>
  );
};
