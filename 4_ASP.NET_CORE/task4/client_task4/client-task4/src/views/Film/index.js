import React from 'react';
import Button from '@material-ui/core/Button';
import { withStyles } from '@material-ui/core/styles/index';
import Card from '@material-ui/core/Card';
import '../../App.css';
import { Link } from 'react-router-dom';
import ReactStars from 'react-stars';
import PropTypes from 'prop-types';
import styles from './style';

let Film = ({ classes, filmPoster, filmName, filmId, filmRating }) => {
	return (
		<Card className={classes.card}>
			<img src={filmPoster} className={classes.media} />
			<h2 className={classes.title}>{filmName}</h2>
			<br />
			<div className={classes.stars}>
				<ReactStars
					count={5}
					value={filmRating / 2}
					size={30}
					edit={false}
					color2={'#ffd700'}
				/>
			</div>
			<br />
			<Button
				variant="raised"
				color="secondary"
				className={classes.button}
				component={Link}
				to={'/film/' + filmId}
			>
				Подробнее
			</Button>
		</Card>
	);
};

Film.propTypes = {
	filmPoster: PropTypes.string.isRequired,
	filmName: PropTypes.string.isRequired,
	filmId: PropTypes.number.isRequired,
	filmRating: PropTypes.number.isRequired,
};

export default withStyles(styles)(Film);
