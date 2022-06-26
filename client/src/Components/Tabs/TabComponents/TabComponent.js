import React, { useContext } from 'react';
import PropTypes from 'prop-types';

import { TabContext } from '..';

const propTypes = {
  component: PropTypes.element,
  index: PropTypes.number
};

function TabComponent({ component, index }){

  const { activeIndex } = useContext(TabContext);

  const tabComponentMarkup = activeIndex === index && component;

  return (
    <div>
      { tabComponentMarkup}
    </div>
  );
}

TabComponent.propTypes = propTypes;

export default TabComponent;