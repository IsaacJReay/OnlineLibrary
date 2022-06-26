import React from 'react';
import PropTypes from 'prop-types';

const propTypes = {
  active: PropTypes.bool,
  onClose: PropTypes.func,
  title: PropTypes.string,
  chilrden: PropTypes.element
};

function Modal({ active, onClose, title, children }){

  const modalMarkup = active && (
    <div style={ styles.modal }>
      <div style={ styles.modalContent }>
        <div style={ styles.modalHeader }>
          <h3>{ title }</h3>
          <button style={ styles.close } onClick={ onClose }>&times;</button>
        </div>
        <div>
          { children }
        </div>
      </div>
    </div>
  );

  return (
    <>
      { modalMarkup }
    </>
  );
}

Modal.propTypes = propTypes

const styles = {
  modal: {
    position: 'absolute',
    width: '100vw',
    height: '100vh',
    zIndex: 20,
    backgroundColor: 'rgba(0, 0, 0, 0.3)',
    display: 'grid',
    placeItems: 'center'
  },
  modalContent: {
    width: '40%',
    padding: '15px',
    backgroundColor: '#fff',
    borderRadius: '5px',
    display: 'flex',
    flexDirection: 'column',
    gap: '20px'
  },
  modalHeader: {
    display: 'flex',
    justifyContent: 'space-between',
    alignItems: 'center'
  },
  close: {
    fontSize: '2rem',
    background: 'none',
    border: 'none',
    cursor: 'pointer'
  }
};

export default Modal;