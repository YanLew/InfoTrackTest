import * as React from "react";
import Pagination from "react-bootstrap/Pagination";

interface IPaginatorProps {
  maxPageNumber: number;
  currentPage: number;
  onPageChanged: (page: number) => void;
}

export const Paginator: React.FunctionComponent<IPaginatorProps> = ({
  maxPageNumber,
  currentPage,
  onPageChanged,
}) => {
  return (
    <Pagination>
      <Pagination.First />
      <Pagination.Prev />
      {[...Array(maxPageNumber).keys()].map((i) => {
        return (
          <Pagination.Item
            active={i + 1 === currentPage}
            onClick={() => {
              onPageChanged(i + 1);
            }}
          >
            {i + 1}
          </Pagination.Item>
        );
      })}
    </Pagination>
  );
};
