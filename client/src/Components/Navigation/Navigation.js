import React from 'react';
import PropTypes from 'prop-types';

const propTypes = {
  name: PropTypes.string.isRequired,
  icon: PropTypes.string.isRequired
};

function Navigation({ name, icon }){
  return (
    <div style={ styles.navigation }>
      <div style={ styles.navigationContent }>
        <img src={ icon } style={ styles.icon } alt="navigation-icon"/>
        <p style={ styles.name }>{ name }</p>
      </div>
      <div style={ styles.navigationIcon }></div>
    </div>
  );
}

Navigation.propTypes = propTypes;

const styles = {
  icon: {
    width: '35px'
  },
  name: {
    color: '#fff',
    fontWeight: 'bold'
  },
  navigation: {
    display: 'flex',
    alignItems: 'center',
    justifyContent: 'space-between',
    padding: '20px',
    borderBottom: '1px solid rgba(255, 255, 255, 0.3)'
  },
  navigationContent: {
    display: 'flex',
    gap: '25px'
  },
  navigationIcon: {
    width: '10px',
    height: '10px',
    borderLeft: '2px solid white',
    borderBottom: '2px solid white',
    transform: 'rotate(235deg)'
  }
};

export default Navigation;