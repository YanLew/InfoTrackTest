import { envConfigs } from "./EnvConfigs";

export const getEnvConfigs = () => {
    return envConfigs;
  };
  
  export const getEnvConfigByName = (name: string, defaultValue?: string): string => {
    const configs = envConfigs as { [key: string]: string | undefined };
    const value = configs[name];
    if (value || defaultValue === undefined) {
      throw new Error(`Appconfig not found: ${name}`);
    }
    return value || defaultValue;
  };
  