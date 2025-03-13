import React, { useEffect, useMemo } from "react";
import useFile, { ReaderResult } from "../../hooks/useFile/useFile";
import DislikeIcon from "../icons/DislikeIcon";

import "./media-input.css";

type MediaDeleteProps = {
  onDelete: () => void;
};

/**
 * MediaDelete component renders a delete icon that calls an `onDelete` function when clicked.
 *
 * @param {object} props - Component props.
 * @param {Function} props.onDelete - Callback function to handle deletion of the media.
 *
 * @returns {React.Element} The MediaDelete component.
 */
function MediaDelete({ onDelete }: MediaDeleteProps) {
  return (
    <div className="media-input__delete" onClick={onDelete}>
      <DislikeIcon></DislikeIcon>
    </div>
  );
}

type MediaInputProps = {
  ref: React.RefObject<HTMLInputElement | null>;
  onChange: () => void;
  onDelete: () => void;
  result: ReaderResult;
};

/**
 * MediaInput component handles file input for media upload (e.g., image or video).
 * It uses a custom hook (`useFile`) to manage the file state and provides
 * options for deleting the uploaded media.
 *
 * @param {object} props - Component props.
 * @param {number} props.keyid - A seed string to ensure unique key for the input element in form field.
 * @param {Function} props.callback - A callback function to pass the file input reference.
 *
 * @returns {React.Element} The MediaInput component.
 */
function MediaInput({ ref, onChange, onDelete, result }: MediaInputProps) {
  const buffer = result ?? undefined;

  return (
    <div className="media-input">
      <div
        className="media-input__box"
        style={{ backgroundImage: buffer && `url("${buffer}")` }}
      >
        <label>
          <input
            name="files"
            ref={ref}
            type="file"
            accept="image/*"
            onChange={onChange}
          />
        </label>
      </div>
      {buffer && <MediaDelete onDelete={onDelete}></MediaDelete>}
      {/* Render delete button if file is uploaded */}
    </div>
  );
}

export default MediaInput;
