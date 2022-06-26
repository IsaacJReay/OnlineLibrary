import { authenticationType } from '../../Constant';

export function setLoginModalVisibleToggle(){
  return { type: authenticationType.SET_LOGIN_MODAL_VISIBLE_TOGGLE };
}

export function setLoginButtonDisable(payload){
  return {
    type: authenticationType.SET_LOGIN_BUTTON_DISABLE,
    payload
  };
}

export function setLoginValidationEmail(payload){
  return {
    type: authenticationType.SET_LOGIN_VALIDATION_EMAIL,
    payload
  };
}

export function setLoginValidationPassword(payload){
  return {
    type: authenticationType.SET_LOGIN_VALIDATION_PASSWORD,
    payload
  };
}

export function setRegisterValidationUsername(payload){
  return {
    type: authenticationType.SET_REGISTER_VALIDATION_USERNAME,
    payload
  };
}

export function setRegisterValidationEmail(payload){
  return {
    type: authenticationType.SET_REGISTER_VALIDATION_EMAIL,
    payload
  };
}

export function setRegisterValidationPassword(payload){
  return {
    type: authenticationType.SET_REGISTER_VALIDATION_PASSWORD,
    payload
  };
}

export function setRegisterValidationPasswordConfirmation(payload){
  return {
    type: authenticationType.SET_REGISTER_VALIDATION_PASSWORD_CONFIRMATION,
    payload
  };
}