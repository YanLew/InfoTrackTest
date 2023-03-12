import { AxiosRequestConfig, AxiosRequestHeaders } from "axios";

export interface IConfigAxiosInterceptorsAsync {
  name: string;
  configs?: AxiosRequestConfig;
  onAxiosInterceptorsFailed?: (errorMessage: string) => void;
  requestHeaders?: AxiosRequestHeaders;
  traceId?: string;
}
