import React, { Fragment, useState } from "react";
import {
  Dialog,
  DialogContent,
  DialogTitle,
  Button,
  IconButton,
} from "@material-ui/core";
import classes from "./OpenDialog.css";

const OpenDialog = ({
  color = "primary",
  icon,
  size,
  buttonText,
  withButton,
  title,
  children,
  ...props
}) => {
  const [open, setOpen] = useState(false);

  const handleClickOpen = () => {
    setOpen(true);
  };

  const handleClose = () => {
    setOpen(false);
  };

  let button = (
    <IconButton aria-label="edit" onClick={handleClickOpen}>
      {icon}
    </IconButton>
  );

  if (withButton) {
    button = (
      <Button
        size={size}
        variant="contained"
        color={color}
        startIcon={icon}
        onClick={handleClickOpen}
      >
        {buttonText}
      </Button>
    );
  }

  return (
    <Fragment>
      {button}
      <Dialog {...props} open={open} onClose={handleClose}>
        <DialogTitle className={classes.DialogTitle}>{title}</DialogTitle>
        <DialogContent className={classes.DialogContent}>
          {children
            ? React.cloneElement(children, { onClose: handleClose, ...props })
            : null}
        </DialogContent>
      </Dialog>
    </Fragment>
  );
};

export default OpenDialog;
