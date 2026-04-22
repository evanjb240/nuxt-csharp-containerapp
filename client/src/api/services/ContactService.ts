/* generated using openapi-typescript-codegen -- do not edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { Contact } from '../models/Contact';
import type { CancelablePromise } from '../core/CancelablePromise';
import { OpenAPI } from '../core/OpenAPI';
import { request as __request } from '../core/request';
export class ContactService {
    /**
     * @param requestBody
     * @returns any OK
     * @throws ApiError
     */
    public static postApiContactSendMessage(
        requestBody?: Contact,
    ): CancelablePromise<any> {
        return __request(OpenAPI, {
            method: 'POST',
            url: '/api/Contact/SendMessage',
            body: requestBody,
            mediaType: 'application/json',
        });
    }
}
