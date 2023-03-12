import * as React from "react";
import { IReactBaseProps } from "./IReactBaseProps";
import "./BaseContainer.scss";

interface IBaseContainerProps extends IReactBaseProps {}

export const BaseContainer: React.FunctionComponent<IBaseContainerProps> = ({
  children,
}) => {
  return <div className="info-track-test-base-container">{children}</div>;
};
