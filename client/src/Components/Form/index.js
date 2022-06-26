import React, { useEffect, useRef } from 'react';
import PropTypes from 'prop-types';
import Input from './Input';

export { Input };

const propTypes = {
  chilrden: PropTypes.element,
  submitTitle: PropTypes.string,
  onSubmit: PropTypes.func,
  submitOptions: PropTypes.shape({
    disable: PropTypes.bool
  })
};

function Form({ children, submitTitle, onSubmit, submitOptions }){
  const buttonRef = useRef(null);

  const disable = submitOptions?.disable;

  useEffect(function(){
    buttonRef.current.style.opacity = disable ? "0.4" : "1";
  }, [disable]);

  const buttonMarkup = submitTitle && (
    <button 
      style={ styles.button } 
      disabled={ disable } 
      ref={ buttonRef }
    >
      { submitTitle }
    </button>
  );

  return (
    <form style={ styles.form } onSubmit={ onSubmit }>
      { children }
      <div style={ styles.buttonContainer  }>
        { buttonMarkup }
      </div>
    </form>
  );
}

Form.propTypes = propTypes;

const styles = {
  form: {
    display: 'flex',
    flexDirection: 'column',
    gap: '15px'
  },
  buttonContainer: {
    display: 'flex',
    justifyContent: 'center'
  },
  button: {
    padding: '5px 20px',
    backgroundColor: '#030c4f',
    color: '#fff',
    fontWeight: 'bold',
    border: 'none',
    borderRadius: '5px',
    fontSize: '1rem',
    width: '30%'
  }
};

export default Form;