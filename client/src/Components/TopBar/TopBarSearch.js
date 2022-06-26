import React, { useEffect, useState } from 'react';
import PropTypes from 'prop-types';

import { IMAGE_PATH } from '../../Constant';

const propTypes = {
  text: PropTypes.string,
  onTextChange: PropTypes.func
}

function TopBarSearch({ text, onTextChange }){

  const [showSearchIcon, setShowSearchIcon] = useState(true);

  useEffect(function(){
    setShowSearchIcon(text === '');
  }, [text]);

  const searchIconMarkup = showSearchIcon && (
    <img src={ IMAGE_PATH.SEARCH_POSTFIX} style={ styles.search } alt="search-icon"/>
  );

  return (
    <div style={ styles.topBarSearch }>
      <input 
        style={ styles.searchBox } 
        placeholder="Search here..."
        value={ text }
        onChange={ onTextChange }
      />
      { searchIconMarkup }
    </div>
  );
}

TopBarSearch.propTypes = propTypes;

const styles = {
  searchBox: {
    padding: '7px',
    border: 'none',
    backgroundColor: '#bfbeb0',
    width: '30vw',
    fontSize: '1rem',
    borderRadius: '7px'
  },
  search: {
    width: '20px',
    position: 'absolute',
    right: '7px',
    top: '7px',
    opacity: '40%'
  },
  topBarSearch: {
    position: 'relative',
    display: 'flex',
    alignItems: 'center',
  }
};

export default TopBarSearch;