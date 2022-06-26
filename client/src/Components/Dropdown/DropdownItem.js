import React, { useRef } from 'react';
import PropTypes from 'prop-types';

const propTypes = {
  name: PropTypes.string,
  onDropdownItemClick: PropTypes.func
};

function DropdownItem({ name, onDropdownItemClick }){

  const dropdownItemRef = useRef(null);

  function handleMouseOver(){
    dropdownItemRef.current.style.backgroundColor = 'rgba(0, 0, 0, 0.1)';
  }

  function handleMouseOut(){
    dropdownItemRef.current.style.backgroundColor = 'transparent';
  }

  return (
    <div 
      style={ styles.dropdownItem } 
      onClick={ onDropdownItemClick } 
      ref={ dropdownItemRef }
      onMouseOver={ handleMouseOver }
      onMouseOut={ handleMouseOut }
    >
      { name }
    </div>
  );
}

DropdownItem.propTypes = propTypes;

const styles = {
  dropdownItem: {
    padding: '10px',
    marginBottom: '10px',
    cursor: 'pointer',
    borderRadius: '5px'
  }
};

export default DropdownItem;