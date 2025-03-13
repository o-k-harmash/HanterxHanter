import { classname } from "../../utils/styles";

import "./selection-input.css";
import React from "react";

export type SelectionValue = string | number | readonly string[] | undefined;

/**
 * Type for a selection element, which consists of an ID and a value.
 *
 * @typedef {Object} SelectionElement
 * @property {number} id - The unique identifier for the selection element.
 * @property {string | number | string[] | undefined} value - The display value of the selection element.
 */
export type SelectionElement = { id: number; value: SelectionValue };

type SelectionTitleProps = {
  value: SelectionValue;
};

/**
 * Component that renders the checked value and its associated check mark.
 *
 * @param {SelectionTitleProps} props - The properties for the component.
 * @returns {JSX.Element} The rendered selection check component.
 */
export function SelectionTitle({ value }: SelectionTitleProps) {
  return <span className="selection-input__title">{value}</span>;
}

/**
 * Component that renders the checked value and its associated check mark.
 *
 * @returns {JSX.Element} The rendered selection check component.
 */
export function SelectionCheck() {
  return <span className="selection-input__check"></span>;
}

type SelectionLabelProps = {
  checked?: boolean;
  children: [
    React.ReactElement<typeof SelectionTitle>,
    React.ReactElement<typeof SelectionCheck>
  ];
  id: string;
};

/**
 * Component that renders a label for a selection input with a check mark.
 * It clones the input and check elements and passes the checked state to them.
 *
 * @param {SelectionLabelProps} props - The properties for the component.
 * @returns {JSX.Element} The rendered label component.
 */
export function SelectionLabel({ checked, children, id }: SelectionLabelProps) {
  // Safe destructuring of the children elements
  const [titleElement, checkElement] = children;

  // Generate the class name based on the checked state
  const className = classname(
    "selection-input__label",
    checked && "selection-input__label_checked"
  );

  return (
    <label className={className} htmlFor={id}>
      {titleElement}
      {checkElement}
    </label>
  );
}

type SelectionGroupProps = {
  children: any;
};

/**
 * Component that renders a group of selection elements.
 *
 * @param {SelectionGroupProps} props - The properties for the component.
 * @returns {JSX.Element} The rendered selection group component.
 */
export function SelectionGroup({ children }: SelectionGroupProps) {
  return <div className="selection-input">{children}</div>;
}

export type SelectionProps = {
  comparison: (value: SelectionElement) => boolean;
  onChange: (value: SelectionElement) => void;
  /** Array of selection elements (id and value) to display in the input group. */
  values: SelectionElement[];
};

/**
 * RadioInput component that renders a group of radio buttons.
 * It uses the `useSingleSelection` hook to handle single selection state.
 *
 * @param {SelectionProps} props - The properties for the component.
 * @param {(value: SelectionElement) => boolean} props.comparison - The name of the selection group.
 * @param {(value: SelectionElement) => void} props.onChange - The name of the selection group.
 * @param {SelectionElement[]} props.values - An array of selection elements to render as radio buttons.
 * @returns {JSX.Element} The rendered radio button input group.
 */
export function SelectionInput({ comparison, values }: SelectionProps) {
  return (
    <SelectionGroup>
      {values.map((gender, index) => (
        <SelectionLabel key={index} checked={comparison(gender)} id="1">
          <SelectionTitle value={gender.value} />
          <SelectionCheck />
        </SelectionLabel>
      ))}
    </SelectionGroup>
  );
}
