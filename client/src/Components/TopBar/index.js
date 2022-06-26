import React, { useState, useCallback } from 'react';

import TopBarLogo from './TopBarLogo';
import TopBarSearch from './TopBarSearch';
import TopBarUserProfile from './TopBarUserProfile';

function TopBar(){

  const [searchText, setSearchText]= useState('');

  const handleSearchTextChange = useCallback(function(event){
    setSearchText(event.target.value);
  }, []);

  return (
    <div style={ styles.topBar }>
      <TopBarLogo />
      <TopBarSearch text={ searchText } onTextChange={ handleSearchTextChange } />
      <TopBarUserProfile />
    </div>
  );
}

const styles = {
  topBar: {
    flex: 1,
    height: '100%',
    backgroundColor: '#d5d1d0',
    display: 'flex',
    alignItems: 'center',
    paddingLeft: '30px',
    paddingRight: '30px',
    justifyContent: 'space-between'
  }
};

export default TopBar;