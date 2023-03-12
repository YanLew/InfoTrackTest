import React from "react";
import "./App.scss";
import { AXIOS_API_SERVER } from "./config/config.constants";
import { envConfigs } from "./config/EnvConfigs";
import { AxiosConfig } from "./core/axios/AxiosConfig";
import Tab from "react-bootstrap/Tab";
import Tabs from "react-bootstrap/Tabs";
import { SearchTab } from "./search/SearchTab";
import { HistoryTab } from "./history/HistoryTab";
import { HistoryDashboardTab } from "./history/HistoryDashboardTab";

interface IAppProps {}

export const App: React.FunctionComponent<IAppProps> = () => {
  return (
    <>
      <AxiosConfig
        axiosInterceptors={[
          {
            name: AXIOS_API_SERVER,
            configs: { baseURL: envConfigs.apiBaseUrl as string },
          },
        ]}
      />
      <Tabs defaultActiveKey="search" id="info-track-test-tabs">
        <Tab eventKey="search" title="Search">
          <SearchTab />
        </Tab>
        <Tab eventKey="history" title="History">
          <HistoryTab />
        </Tab>
        <Tab eventKey="history-dashboard" title="History Dashboard">
          <HistoryDashboardTab />
        </Tab>
      </Tabs>
    </>
  );
};
