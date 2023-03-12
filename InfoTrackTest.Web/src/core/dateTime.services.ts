import moment from "moment";

export const formatIsoDateTimeString = (dateTimeStr: string) => {
  return moment(dateTimeStr).format("YYYY-MM-DD hh:mm:ss");
};
