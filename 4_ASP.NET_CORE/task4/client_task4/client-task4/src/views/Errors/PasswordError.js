import React from 'react';
import { withStyles } from '@material-ui/core/styles/index';
import styles from './style';
import PropTypes from 'prop-types';

export let PasswordError = ({ classes }) => {
	return <div className={classes.error}>Слишком короткий пароль</div>;
};
PasswordError.propTypes = {
	classes: PropTypes.object.isRequired,
};
export default withStyles(styles)(PasswordError);
