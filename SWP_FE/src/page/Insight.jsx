import React, { useEffect, useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import useAuth from '../hooks/useAuth';
import api from '../components/utils/requestAPI';
import Na from "./Napage";
import "./Insight.css";

export default function Insight() {
  const [shippingMethod, setShippingMethod] = useState('');
  const [productInfo, setProductInfo] = useState(null);
  const [orderList, setOrderList] = useState([]);
  const [loading, setLoading] = useState(true);
  const [orders, setOrders] = useState([]);
  const { auth } = useAuth();
  const [user, setUser] = useState(null);
  const [artworkList, setArtworkList] = useState([]);
  const [payment, setPayment] = useState([]);
  const [userArtworkInfo, setUserArtworkInfo] = useState(null);
  const navigate = useNavigate();
  let paymentUpdated = false;
  let orderUpdated = false;

  useEffect(() => {
    const fetchUserData = async () => {
      try {
        if (auth.user) {
          const response = await api.post("https://localhost:7227/api/User/get-by-id", { userId: auth.user.userId });
          setUser(response.data);
          const responseArtworks = await api.get("https://localhost:7227/api/Order/get-all");
          const allOrders = responseArtworks.data.$values;
          const userOrders = allOrders.filter(order => order.userId === auth.user.userId);
                
      // Gọi API để lấy thông tin moder
      const userresponse = await api.get(`https://localhost:7227/api/User/get all user`);
                const users = userresponse.data.$values;
                const userIdsWithRoleId = users.filter(user => user.roleId === `4`);
                const userIdModer = userIdsWithRoleId.map(user => user.userId);
                console.log(userIdModer);
                const userId_artwork_Respone = await api.post(`https://localhost:7227/api/User/get-by-id`, { userID: userIdModer[1] });
                const userId_artwork_data = userId_artwork_Respone.data;
                console.log(userId_artwork_data);
                
                
                setUserArtworkInfo(userId_artwork_data);

          
          setOrders(userOrders);
          setLoading(false);
        }
      } catch (error) {
        console.error('Error fetching user data:', error);
      }
    };

    fetchUserData();
  }, [auth]);
  useEffect(() => {
    const fetchArtworkData = async () => {
      try {
        const artworkPromises = orders.map(ord => api.get(`https://localhost:7227/api/Artwork/get-by-id?id=${ord.artworkId}`));
        const artworks = await Promise.all(artworkPromises);
        const userIds = artworks.map(artwork => artwork.data.userId);
      
      // Lấy tên người dùng từ userIds
      const userNames = await fetchUserNames(userIds);
      
      const artworkList = artworks.reduce((acc, artwork, index) => {
        const userId = artwork.data.userId;
        const userName = userNames[index]; // Lấy tên người dùng tương ứng với userId
        acc[orders[index].artworkId] = {
          ...artwork.data,
          userName: userName // Thêm thông tin tên người dùng vào đối tượng artwork
        };
        return acc;
        }, {});
        setArtworkList(artworkList);
      } catch (error) {
        console.error('Error fetching artwork data:', error);
      }
    };

    if (orders.length > 0) {
      fetchArtworkData();
    }
  }, [orders]);

  const handleShippingMethodChange = (method) => {
    setShippingMethod(method);
    setShowShippingInfo(method === 'Shipping');
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    // sau khi submit form
  };
  
  useEffect(() => {
    if (auth.user) {
      // Nếu auth.user đã được thiết lập, thực hiện các thao tác tiếp theo
      const storedNotification = localStorage.getItem(`notification_${auth.user.userId}`);
      if (storedNotification) {
        alert(storedNotification);
        localStorage.removeItem(`notification_${auth.user.userId}`);
      }
    }
  }, [auth.user]);
  const fetchUserNames = async (userIds) => {
    try {
      const promises = userIds.map(userId => api.post('https://localhost:7227/api/User/get-by-id', { userId }));
      const responses = await Promise.all(promises);
      const userNames = responses.map(response => response.data.username);
      return userNames;
    } catch (error) {
      console.error('Error fetching user names:', error);
      return [];
    }
  }; 

  const Refund = async (ord) => {
    try {
      const Paymentresponse = await api.get(`https://localhost:7227/api/Payment/get-payment-by-order-id?id=${ord}`);
      const PaymentCancle = await api.post(`https://localhost:7227/api/Payment/delete-payment?id=${Paymentresponse.data.paymentId}`);
      
    alert("You refunded artwork, money will be refunded to your account after verification ");
    navigate("/home");
    } catch (error) {
      console.error('Error refund:', error);
    }
  };

  useEffect(() => {
  const fetchOrderData2 = async () => {
    try {
      const orderPromises = orders.map(order => api.get(`https://localhost:7227/api/Payment/get-payment-by-order-id?id=${order.orderId}`));
      const orderResponses = await Promise.all(orderPromises);
      const orderList = orderResponses.reduce((acc, order, index) => {
        acc[orders[index].paymentId] = order.data;
              return acc;
      }, {});

      setOrderList(orderList);
      
    } catch (error) {
      console.error('Error fetching order data:', error);
    }
  };

  if (orders.length > 0) {
    fetchOrderData2();
  }
}, [orders]);
// console.log(artworkList);


  return (
    <div>
      <Na className="Navuser" /> 
    
   
      
      
          
          <div className="insight-order-box">
            {orders.map((ord) => (
              ord.statusCancel && 
              // orderList[ord.orderId] &&
              artworkList[ord.artworkId] && 
              <div key={ord.$id} className="insight-image-collection">
                
                <div className="insight-order-overlay">  
   <img src={artworkList[ord.artworkId].imageUrl2} alt="insight-Artwork" 
   className={ord.status ? '' : 'processing-false'}
   />
   {!ord.status && <div className="waiting-text">Chờ duyệt</div>}
   {/* { ord.statusCancel &&  <div className="cancel-waiting-text">Chờ hoàn tiền</div>} */}
   
                </div>
                <div className="insight-order-details">
                  <div className="insight-order-authors">{artworkList[ord.artworkId].userName}</div>
                  <div className="insight-order-titles">{artworkList[ord.artworkId].title}</div>
                  {/* {!item.status && ( 
                            <button onClick={() => handleConfirmOrder(auth.user.userId,artworkList[item.artworkId].userId,item.orderId)}><div className="recieve-StatusApprove">Confirm</div></button>
                            )} */}
                 {!ord.status && (
                 <button onClick={() => Refund(ord.orderId)}>Refund Artwork</button> )}
                </div>
                
              </div>
              // ))
            ))}
          </div>
          
        
      
   </div>
  );
}