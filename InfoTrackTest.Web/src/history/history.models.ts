export interface IHistory {
  id: number;
  keyword: string;
  rank: string;
  searchEngineName: string;
  createdDateTime: string;
}

export interface IHistoryDashboard {
  keyword: string;
  searchEngineName: string;
  averageHighestRank: number;
  highestRank: number;
  notFoundCount: number;
  totalSearchCount: number;
  date: string;
}
