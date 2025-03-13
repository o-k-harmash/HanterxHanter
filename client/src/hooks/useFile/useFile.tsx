import { useState, useCallback, useRef } from "react";

// Initial value for file blobs
const INIT_READER_VALUE: ReaderResult = null;

const INIT_FILE_VALUE: FileResult = null;

// Type definition for file reading result
export type ReaderResult = string | ArrayBuffer | null;

export type FileResult = File | null;

/**
 * Asynchronously reads a file and returns the result as a base64 string or ArrayBuffer.
 * @param file - The file to be read.
 * @returns A Promise that resolves to the file result (base64 string or ArrayBuffer).
 */
export function readFileAsync(file: File): Promise<ReaderResult> {
  return new Promise((resolve) => {
    const reader = new FileReader();
    reader.onloadend = () => resolve(reader.result);
    reader.readAsDataURL(file);
  });
}

/**
 * Custom hook for managing file state and operations on a file input.
 * @param fileRef - A reference to the file input element.
 * @returns Object containing the current file blobs and handler functions for file input events.
 */
export default function useFile() {
  // Ref to file
  const ref = useRef<HTMLInputElement | null>(null);
  // State to store file blobs
  const [result, setResult] = useState<ReaderResult>(INIT_READER_VALUE);

  // State to store file
  const [file, setFile] = useState<FileResult>(INIT_FILE_VALUE);

  /**
   * Handles the file input change event, reads the selected files, and updates the state with the file blobs.
   * Asynchronously reads the files and stores their base64 data.
   */
  const onChange = useCallback(async () => {
    const files = ref.current?.files;
    if (files) {
      const file = files[0];
      const results = await readFileAsync(file);
      setFile(file);
      setResult(results);
    }
  }, []);

  /**
   * Clears the selected files in the file input and resets the state.
   */
  const onDelete = useCallback(() => {
    const curr = ref.current;
    if (curr) {
      curr.value = ""; // Clears the file input
    }
    setFile(INIT_FILE_VALUE);
    setResult(INIT_READER_VALUE); // Resets the file blob state
  }, []);

  return {
    ref, // The current file ref
    file,
    result, // The current file blobs state
    onChange, // Function to handle file input changes
    onDelete, // Function to clear the file input and state
  };
}
