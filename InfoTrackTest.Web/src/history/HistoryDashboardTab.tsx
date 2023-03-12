import * as React from "react";
import Table from "react-bootstrap/Table";
import { BaseContainer } from "../core/BaseContainer";
import { useFetchHistoryDashboard } from "./history.queries";
import { formatIsoDateTimeString } from "../core/dateTime.services";

interface IHistoryDashboardTabProps {}

export const HistoryDashboardTab: React.FunctionComponent<
  IHistoryDashboardTabProps
> = () => {
  const { data: historyDashboards, isLoading } = useFetchHistoryDashboard();

  return (
    <BaseContainer>
      <Table striped bordered hover>
        <thead>
          <tr>
            <th>Date</th>
            <th>Search Engine</th>
            <th>Keywords</th>
            <th>Total Count</th>
            <th>No Found Count</th>
            <th>Average Highest Rank</th>
            <th>Highest Rank</th>
          </tr>
        </thead>
        <tbody>
          {(historyDashboards || []).map((historyDashboard, index) => {
            return (
              <tr
                key={`${historyDashboard.date}-${historyDashboard.searchEngineName}-${historyDashboard.keyword}`}
              >
                <td>{formatIsoDateTimeString(historyDashboard.date)}</td>
                <td>{historyDashboard.searchEngineName}</td>
                <td>{historyDashboard.keyword}</td>
                <td>{historyDashboard.totalSearchCount}</td>
                <td>{historyDashboard.notFoundCount}</td>
                <td>{historyDashboard.averageHighestRank}</td>
                <td>{historyDashboard.highestRank}</td>
              </tr>
            );
          })}
        </tbody>
      </Table>
    </BaseContainer>
  );
};
