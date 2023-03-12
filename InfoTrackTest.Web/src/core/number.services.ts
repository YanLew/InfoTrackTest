export const isPositiveNumber = (
  num: number | undefined,
  allowZero = false
): boolean => {
  return num !== undefined && (num > 0 || (allowZero && num === 0));
};
