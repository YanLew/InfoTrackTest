import * as React from "react";
import { IReactBaseProps } from "../IReactBaseProps";
import { IConfigAxiosInterceptorsAsync } from "./axios.models";
import { configAxiosInterceptorsAsync } from "./axios.services";

export interface IAxiosConfigProps extends IReactBaseProps {
  axiosInterceptors: IConfigAxiosInterceptorsAsync[];
}

export const AxiosConfig: React.FunctionComponent<IAxiosConfigProps> = ({
  children,
  axiosInterceptors,
}) => {
  const [isConfiguring, setIsConfiguring] = React.useState(true);
  React.useEffect(() => {
    async function start() {
      axiosInterceptors.map(async ({ ...rest }) => {
        await configAxiosInterceptorsAsync(rest);
      });
      setIsConfiguring(false);
    }
    start();
  }, [JSON.stringify(axiosInterceptors)]);
  return !isConfiguring ? <>{children}</> : null;
};
