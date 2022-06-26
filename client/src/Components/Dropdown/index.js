import React from 'react';
import { v4 as uuidv4 } from 'uuid';
import PropTypes from 'prop-types';

import DropdownItem from './DropdownItem';

const propTypes = {
  children: PropTypes.element,
  actions: PropTypes.arrayOf(PropTypes.shape({
    name: PropTypes.string,
    onAction: PropTypes.func
  })),
  active: PropTypes.bool,
  onClick: PropTypes.func
};

function Dropdown({ children, actions, active, onClick }){

  const modalActions = actions.map(function(action){
    return (
      <DropdownItem key={ uuidv4() } name={ action.name} onDropdownItemClick={ action.onAction } />
    );
  });

  const modalActionsMarkup = active && (
    <div style={ styles.dropdownActions }>
      { modalActions }
    </div>
  );

  return (
    <div style={ styles.dropdown } onClick={ onClick }>
      { children }
      { modalActionsMarkup }
    </div>
  );
}

Dropdown.propTypes = propTypes;

const styles = {
  dropdownActions: {
    position: 'absolute',
    backgroundColor: 'white',
    padding: '20px',
    paddingTop: '10px',
    paddingBottom: 0,
    right: 0,
    width: '200px',
    borderRadius: '5px',
    boxShadow: '0px 0px 5px rgba(0, 0, 0, 0.3)',
    zIndex: 10
  },
  dropdown: {
    position: 'relative',
  }
};

export default Dropdown;