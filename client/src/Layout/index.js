import React, { useCallback } from 'react';
import PropTypes from 'prop-types';
import { useSelector, useDispatch } from 'react-redux';

import { IMAGE_PATH } from '../Constant';
import Navigation from '../Components/Navigation';
import TopBar from '../Components/TopBar';
import Modal from '../Components/Modal';
import { setLoginModalVisibleToggle } from '../store/action/authentication';
import Tabs from '../Components/Tabs';
import {  Login, Register } from '../Components/Authentication';

const propTypes = {
  children: PropTypes.element
};

function Layout({ children }){

  const loginModalVisible = useSelector(state => state.authentication.loginModalVisible);
  const dispatch = useDispatch();

  const handleLoginModalVisibleToggle = useCallback(function(){
    dispatch(setLoginModalVisibleToggle());
  }, [dispatch]);

  return(
    <div style={ styles.layout }>
      <Modal 
        active={ loginModalVisible } 
        onClose={ handleLoginModalVisibleToggle }
        title="Login/Register"
      >
        <Tabs data={[
          {
            title: 'Login',
            component: <Login />
          },
          {
            title: 'Register',
            component: <Register />
          }
        ]} />
      </Modal>
      <div style={ styles.navigation }>
        <div style={ styles.navigationHeader }>
          <img src={ IMAGE_PATH.LOGO } alt="logo"/>
        </div>
        <div style={ styles.navigationSection }>
          <Navigation />
        </div>
      </div>
      <div style={ styles.content }>
        <div style={ styles.topbar}>
          <TopBar />
        </div>
        <div style={ styles.mainContent }>
          <div style={ styles.backgroundImage }></div>
          { children } 
        </div>
      </div>
    </div>
  )
}

Layout.propTypes = propTypes;

const styles = {
  layout: {
    display: 'flex',
    height: '100vh'
  },
  navigation: {
    display: 'flex',
    flexDirection: 'column',
    flex: 1,

  },
  navigationHeader: {
    backgroundColor: '#f8ab00',
    display: 'flex',
    alignItems: 'center',
    justifyContent: 'center',
    fontSize: '1.3rem',
    fontWeight: 'bold',
    color: 'white',
    height: '80px'
  },
  navigationSection: {
    flex: 1,
    backgroundColor: '#030c4f'
  },
  content: {
    flex: 4,
    display: 'flex',
    flexDirection: 'column'
  },
  topbar: {
    height: '80px',
  },
  mainContent: {
    flex: 1,
    position: 'relative',
    overflow: 'hidden',
  },
  backgroundImage: {
    position: 'absolute',
    width: '100%',
    height: '100%',
    zIndex: -1,
    opacity: 0.4,
    backgroundImage: `url(${ IMAGE_PATH.BACKGROUND_IMAGE })`
  }
}

export default Layout;