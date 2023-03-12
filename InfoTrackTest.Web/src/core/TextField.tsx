import * as React from "react";
import Form from "react-bootstrap/Form";

interface ITextFieldProps {
  value?: string | undefined;
  onChange?: (newValue?: string | undefined) => void;
  debounceTime?: number;
  shouldDebounce?: boolean;
  placeholder?: string | undefined;
}

export const TextField: React.FunctionComponent<ITextFieldProps> = ({
  value,
  onChange,
  debounceTime = 2000,
  shouldDebounce = true,
  placeholder,
}) => {
  const [localValue, setLocalValue] = React.useState<string | undefined>(
    undefined
  );

  React.useEffect(() => {
    setLocalValue(value);
  }, [value]);

  const onChangeValue = (e: React.FormEvent<EventTarget>) => {
    const target = e.target as HTMLInputElement;
    const value = target.value;
    setLocalValue(value);
    if (!shouldDebounce && onChange) onChange(value);
  };

  React.useEffect(() => {
    if (
      !shouldDebounce ||
      !onChange ||
      localValue === undefined ||
      localValue === null
    )
      return;

    const handler = setTimeout(() => {
      onChange(localValue);
    }, debounceTime);
    return () => {
      clearTimeout(handler);
    };
  }, [localValue]);

  return (
    <Form.Control
      type="text"
      placeholder={placeholder}
      value={localValue}
      onChange={onChangeValue}
    />
  );
};
