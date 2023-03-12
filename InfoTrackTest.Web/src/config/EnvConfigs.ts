import { REACT_APP_PREFIX } from "./config.constants";

export interface IEnvConfigs {
  apiBaseUrl?: string;
  env?: string;
}

export const envConfigs: IEnvConfigs = {
  apiBaseUrl: process.env[`${REACT_APP_PREFIX}API_BASE_URL`],
  env: process.env[`${REACT_APP_PREFIX}ENV`],
};
