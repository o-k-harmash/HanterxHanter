import IconButton from "../../../components/IconButton";

type ActionsFooterProps = {
  like: () => void;
  dislike: () => void;
  superlike: () => void;
};

function ActionFooter({ like, dislike, superlike }: ActionsFooterProps) {
  return (
    <div className="profile-actions-footer fixed vertical-center">
      <IconButton type="dislike" style="style-1" onClick={dislike}></IconButton>
      <IconButton
        type="superlike"
        style="style-1"
        onClick={superlike}
      ></IconButton>
      <IconButton type="like" style="style-1" onClick={like}></IconButton>
    </div>
  );
}

export default ActionFooter;
