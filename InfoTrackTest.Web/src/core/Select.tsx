import * as React from "react";
import Form from "react-bootstrap/Form";

interface ISelectProps<TOption> {
  options: TOption[];
  idFieldName: string;
  displayFieldName: string;
  onSelected?: (selectedValue: string | undefined) => void;
  defalutOption?: string | undefined;
}

export const Select: <TOption>(
  props: ISelectProps<TOption>
) => React.ReactElement<ISelectProps<TOption>> = ({
  options,
  idFieldName,
  displayFieldName,
  onSelected,
  defalutOption,
}) => {
  const [localValue, setLocalValue] = React.useState<string | undefined>(
    undefined
  );

  const onChangeValue = (e: React.FormEvent<EventTarget>) => {
    const target = e.target as HTMLSelectElement;
    const value = target.value;
    setLocalValue(value);
    if (onSelected) onSelected(value);
  };

  return (
    <>
      <Form.Select
        onChange={onChangeValue}
        defaultValue={
          !!defalutOption ? (defalutOption as any)[idFieldName] : undefined
        }
        value={localValue}
      >
        {(options || []).map((option) => {
          return (
            <option
              key={(option as any)[idFieldName]}
              value={(option as any)[idFieldName]}
            >
              {(option as any)[displayFieldName]}
            </option>
          );
        })}
      </Form.Select>
    </>
  );
};
