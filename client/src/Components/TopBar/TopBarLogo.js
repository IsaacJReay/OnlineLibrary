import React from 'react';

import { IMAGE_PATH } from '../../Constant';

function TopBarLogo(){
  return (
    <div style={ styles.topBarLogo }>
      <img src={ IMAGE_PATH.BLACK_LOGO } style={ styles.logo } alt="black-logo"/>
      <p>Welcome to Library Management System</p>
    </div>
  );
}

const styles = {
  logo: {
    width: '45px'
  },
  topBarLogo: {
    display: 'flex',
    alignItems: 'center',
    gap: '2px',
    opacity: '40%'
  }
};

export default TopBarLogo;