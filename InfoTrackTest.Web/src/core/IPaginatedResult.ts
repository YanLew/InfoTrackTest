export interface IPaginagedResult<T> {
  items: T[];
  page: number;
  pageSize: number;
  totalCount: number;
}
