import React, { useRef, useEffect, useContext } from 'react';
import PropTypes from 'prop-types'

import { TabContext } from '..';

const propTypes = {
  title: PropTypes.string,
  index: PropTypes.number,
};

function TabHeader({ title, index }){
  const tabHeaderRef = useRef(null);
  const tabHeaderTitleRef = useRef(null);
  const { activeIndex, handleActiveIndexChange } = useContext(TabContext);

  useEffect(function(){
    if(activeIndex === index){
      tabHeaderRef.current.style.backgroundColor = '#030c4f';
      tabHeaderTitleRef.current.style.color = '#fff';
    }else{
      tabHeaderRef.current.style.backgroundColor = '#f2f4f1';
      tabHeaderTitleRef.current.style.color = '#000';
    }
  }, [activeIndex, index]);

  function handleClick(){
    handleActiveIndexChange(index);
  }

  return (
    <div style={ styles.tabHeader } ref={ tabHeaderRef } onClick={ handleClick }>
      <p style={ styles.tabHeaderTitle } ref={ tabHeaderTitleRef }>{ title }</p>
    </div>
  );
}

TabHeader.propTypes = propTypes;

const styles = {
  tabHeader: {
    display: 'flex',
    flex: 1,
    justifyContent: 'center',
    padding: '8px',
    cursor: 'pointer'
  },
  tabHeaderTitle: {
    fontSize: '1.2rem',
    fontWeight: 'bold'
  }
};

export default TabHeader;