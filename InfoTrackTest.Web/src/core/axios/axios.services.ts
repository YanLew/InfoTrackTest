import Axios, {
  AxiosError,
  AxiosInstance,
  AxiosRequestConfig,
  ParamsSerializerOptions,
} from "axios";
import * as qs from "qs";
import { IConfigAxiosInterceptorsAsync } from "./axios.models";

interface IAxiosApiInstanceMapper {
  [key: string]: AxiosInstance;
}

const axiosApiInstanceMapper: IAxiosApiInstanceMapper = {};

export const getAxiosApiInstanceByName = (
  name: string,
  configs?: AxiosRequestConfig
): AxiosInstance => {
  if (!axiosApiInstanceMapper) throw new Error("No axios instance configured");
  if (!axiosApiInstanceMapper[name])
    throw new Error(`Cannot find axios instance by name: ${name}`);
  return axiosApiInstanceMapper[name];
};

export const configAxiosInterceptorsAsync = ({
  name,
  configs,
  traceId,
  onAxiosInterceptorsFailed,
  requestHeaders,
}: IConfigAxiosInterceptorsAsync): void => {
  const axiosApiInstance = Axios.create(configs);
  axiosApiInstanceMapper[name] = axiosApiInstance;
  // Request interceptor for API calls
  axiosApiInstance.interceptors.request.use((config) => {
    try {
      let headers: any = traceId
        ? {
            "X-TraceId": traceId,
            Accept: "application/json",
          }
        : { Accept: "application/json" };
      headers = { ...headers, ...(requestHeaders ? requestHeaders : {}) };
      config.headers = headers;
      config.paramsSerializer = defaultParamsSerializer;
      return config;
    } catch (ex) {
      const error = ex as Error | AxiosError;
      if (
        onAxiosInterceptorsFailed &&
        typeof onAxiosInterceptorsFailed === "function"
      ) {
        onAxiosInterceptorsFailed(error.message);
      }
      return config;
    }
  }, defaultOnRejected);
};

const defaultParamsSerializer = {
  serialize: (params: Record<string, any>, options?: ParamsSerializerOptions) =>
    qs.stringify(params, {
      arrayFormat: "indices",
      allowDots: true,
      encode: false,
    }),
} as ParamsSerializerOptions;

const defaultOnRejected = (error: any) => {
  Promise.reject(error);
};
