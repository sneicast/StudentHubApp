export class ErrorHandlerUtil {
  static handleError(error: any, customMessage: string): string {
    if (error.error && error.error.message) {
      return error.error.message;
    } else if (error.message) {
      return error.message;
    }  else {
      return customMessage || 'Error desconocido. Por favor intenta de nuevo.';
    }
  }
}