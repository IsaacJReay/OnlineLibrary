import React, { useState } from 'react';
import { useDispatch } from 'react-redux';

import { IMAGE_PATH } from '../../Constant';
import Dropdown from '../Dropdown';
import { setLoginModalVisibleToggle } from '../../store/action/authentication';

function TopBarUserProfile(){

  const [dropdownActive, setDropdownActive] = useState(false);
  const dispatch = useDispatch();

  function handleDropdownActiveToggle(){
    setDropdownActive(state => !state);
  }

  return (
    <Dropdown
      actions={[
        {
          name: 'Login',
          onAction: () => dispatch(setLoginModalVisibleToggle())
        }
      ]}
      active={ dropdownActive }
      onClick={ handleDropdownActiveToggle }
    >
      <img 
        src={ IMAGE_PATH.DEFAULT_USER_PROFILE } 
        style={ styles.userProfile } alt="user-profile" 
      />
    </Dropdown>
  );
}

const styles = {
  userProfile: {
    width: '50px',
    cursor: 'pointer'
  }
};

export default TopBarUserProfile;