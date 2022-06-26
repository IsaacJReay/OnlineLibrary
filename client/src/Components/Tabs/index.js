import React, { useEffect, useState, createContext } from 'react';
import PropTypes from 'prop-types';

import TabHeaders from './TabHeaders';
import TabComponents from './TabComponents';

export const TabContext = createContext();

const propTypes = {
  data: PropTypes.arrayOf(PropTypes.shape({
    title: PropTypes.string,
    component: PropTypes.element
  }))
};

function Tabs({ data }){

  const [headers, setHeaders] = useState([]);
  const [components, setComponents] = useState([]);
  const [activeIndex, setActiveIndex] = useState(0);

  useEffect(function(){
    const headers = data.map(function(d, idx){
      return {
        name: d.title,
        index: idx
      };
    });

    const components = data.map(function(d, idx){
      return {
        element: d.component,
        index: idx
      }
    });

    setHeaders(headers);

    setComponents(components);
  }, [data]);

  function handleActiveIndexChange(index){
    setActiveIndex(index);
  }

  return (
    <TabContext.Provider value={{ activeIndex, handleActiveIndexChange }}>
      <div style={ styles.tab }>
        <TabHeaders titles={ headers } />
        <TabComponents components={ components }  />
      </div>
    </TabContext.Provider>
  );
}

Tabs.propTypes = propTypes;

const styles = {
  tab: {
      display: 'flex',
      flexDirection: 'column',
      gap: '20px'
  }
};

export default Tabs;