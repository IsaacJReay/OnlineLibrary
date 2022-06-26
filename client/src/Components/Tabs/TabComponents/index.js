import React from 'react';
import PropTypes from 'prop-types';
import { v4 as uuidv4 } from 'uuid';

import TabComponent from './TabComponent';

const propTypes = {
  components: PropTypes.arrayOf(PropTypes.shape({
    element: PropTypes.element,
    index: PropTypes.number
  }))
};

function TabComponents({ components }){

  const tabComponents = components.map(function(component){
    return (
      <TabComponent 
      component={ component.element } 
      index={ component.index } 
      key={ uuidv4() } />
    )
  });

  return (
    <div>
      { tabComponents }
    </div>
  );
}

TabComponents.propTypes = propTypes;

export default TabComponents;