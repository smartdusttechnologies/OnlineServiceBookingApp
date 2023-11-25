import React, { useContext } from 'react'
import { View, Text } from 'react-native'
import { TextDefault } from '..'
import styles from './styles'
import { colors } from '../../utilities'
import { Configuration } from '../../ui/context'

export default function OrderDetails({ orderData }) {
  const { orderId, user, deliveryAddress } = orderData

  return (
    <View style={{ flex: 1 }}>
      <View style={styles.cardContainer}>
        <View style={styles.row}>
          <Text style={styles.heading}>Order No.</Text>
          <Text style={styles.text} selectable>
            {orderId}
          </Text>
        </View>
        <View style={styles.row}>
          <Text style={styles.heading}>Email</Text>
          <Text style={styles.text} selectable>
            {user.email}
          </Text>
        </View>
        <View style={styles.row}>
          <Text style={styles.heading}>Contact</Text>
          <Text style={styles.text} selectable>
            {user.phone}
          </Text>
        </View>
        <View style={styles.row}>
          <Text style={styles.heading}>Address</Text>
          <Text style={styles.text} selectable>
            {deliveryAddress.deliveryAddress}
          </Text>
        </View>
      </View>
      <OrderItems orderData={orderData} />
    </View>
  )
}
function OrderItems({ orderData }) {
  const {
    items,
    orderAmount,
    tipping,
    deliveryCharges,
    taxationAmount
  } = orderData
  const configuration = useContext(Configuration.Context)
  let subTotal = 0
  return (
    <View style={[styles.cardContainer, { marginTop: 30, marginBottom: 45 }]}>
      {items &&
        items.map((item, index) => {
          subTotal = subTotal + item.variation.price
          return (
            <View style={styles.itemRowBar} key={index}>
              <TextDefault
                H5
                textColor={colors.fontSecondColor}
                bold>{`${item.quantity}x ${item.title}`}</TextDefault>
              <TextDefault
                bold>{`${configuration.currencySymbol}${item.variation.price}`}</TextDefault>
              {item.addons &&
                item.addons.map((addon, index) => {
                  ;<TextDefault
                    H6>{`${configuration.currencySymbol}${addon.price}`}</TextDefault>
                })}
            </View>
          )
        })}
      <View style={styles.itemRow}>
        <TextDefault
          H6
          textColor={colors.fontSecondColor}
          bold
          style={styles.itemHeading}>
          Subtotal
        </TextDefault>
        <TextDefault bold style={styles.itemText}>
          {`${configuration.currencySymbol}${subTotal.toFixed(2)}`}
        </TextDefault>
      </View>
      <View style={styles.itemRow}>
        <TextDefault
          H6
          textColor={colors.fontSecondColor}
          bold
          style={styles.itemHeading}>
          Tip
        </TextDefault>
        <TextDefault bold style={styles.itemText}>
          {`${configuration.currencySymbol}${tipping}`}
        </TextDefault>
      </View>
      <View style={styles.itemRow}>
        <TextDefault
          H6
          textColor={colors.fontSecondColor}
          bold
          style={styles.itemHeading}>
          Tax Charges
        </TextDefault>
        <TextDefault bold style={styles.itemText}>
          {`${configuration.currencySymbol}${taxationAmount}`}
        </TextDefault>
      </View>
      <View style={styles.itemRow}>
        <TextDefault
          H6
          textColor={colors.fontSecondColor}
          bold
          style={styles.itemHeading}>
          Delivery Charges
        </TextDefault>
        <TextDefault bold style={styles.itemText}>
          {`${configuration.currencySymbol}${deliveryCharges}`}
        </TextDefault>
      </View>

      <View style={[styles.itemRow, { marginTop: 30 }]}>
        <TextDefault
          H6
          textColor={colors.fontSecondColor}
          bold
          style={styles.itemHeading}>
          Total
        </TextDefault>
        <TextDefault bold style={styles.itemText}>
          {`${configuration.currencySymbol}${orderAmount}`}
        </TextDefault>
      </View>
    </View>
  )
}
