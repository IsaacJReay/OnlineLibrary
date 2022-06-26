import React from 'react';
import PropTypes from 'prop-types';
import { v4 as uuidv4 } from 'uuid';

import TabHeader from './TabHeader';

const propTypes = {
  titles: PropTypes.arrayOf(PropTypes.shape({
    name: PropTypes.string,
    index: PropTypes.number
  }))
};

function TabHeaders({ titles }){

  const tabHeaders = titles.map(function(title){
    return (<TabHeader 
      title={ title.name} 
      key={ uuidv4() } 
      index={ title.index } 
    />);
  });

  return (
    <div style={ styles.tabHeaders }>
      { tabHeaders }
    </div>
  );
}

TabHeaders.propTypes = propTypes;

const styles = {
  tabHeaders: {
    display: 'flex',
    borderRadius: '5px',
    overflow: 'hidden'
  }
};

export default TabHeaders;