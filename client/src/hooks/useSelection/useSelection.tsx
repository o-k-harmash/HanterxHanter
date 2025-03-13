import { useState, useCallback } from "react";

export interface UseSelection<T> {
  comparison: (value: T) => boolean;
  onChange: (value: T) => void;
}

export interface UseSingleSelection<T> {
  selected: T | undefined;
}

/**
 * Custom hook for handling single selection logic.
 *
 * @template T The type of the selected item.
 * @param {T} initialValue The initial value of the selected item.
 * @returns {UseSelection<T> & UseSingleSelection<T>} The hook returns functions and values related to selection.
 */
export function useSingleSelection<T>(
  initialValue?: T
): UseSelection<T> & UseSingleSelection<T> {
  const [selected, setSelected] = useState<T | undefined>(initialValue);

  /**
   * Handles the selection change.
   *
   * @param {T} value The new value to be selected.
   */
  const onChange = useCallback((value: T) => {
    setSelected(value);
  }, []);

  /**
   * Compares the provided value with the selected value.
   *
   * @param {T} value The value to compare with the selected value.
   * @returns {boolean} True if the provided value matches the selected value, false otherwise.
   */
  const comparison = (value: T) => {
    return selected === value;
  };

  return {
    comparison,
    selected,
    onChange,
  };
}

export interface UseMultipleSelection<T> {
  selected: T[];
}

/**
 * Custom hook for handling multiple selection logic.
 *
 * @template T The type of items being selected.
 * @param {T[]} initialValue The initial selected items.
 * @returns {UseSelection<T> & UseMultipleSelection<T>} The hook returns functions and values related to multiple selection.
 */
export function useMultipleSelection<T>(
  initialValue?: T[]
): UseSelection<T> & UseMultipleSelection<T> {
  const [selected, setSelected] = useState<T[]>(initialValue ?? []);

  /**
   * Handles the selection change for multiple items.
   *
   * @param {T} value The item to add or remove from the selected list.
   */
  const onChange = (value: T) => {
    setSelected((prevSelected) => {
      if (comparison(value)) {
        return prevSelected.filter((item) => item !== value);
      } else {
        return [...prevSelected, value];
      }
    });
  };

  /**
   * Compares the provided value with the current selected values.
   *
   * @param {T} value The value to check if it is selected.
   * @returns {boolean} True if the value is in the selected list, false otherwise.
   */
  const comparison = (value: T) => {
    return selected.includes(value);
  };

  return {
    comparison,
    selected,
    onChange,
  };
}
