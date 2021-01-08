package com.li.jrm;

import net.sf.cglib.proxy.Enhancer;
import net.sf.cglib.proxy.MethodInterceptor;
import net.sf.cglib.proxy.MethodProxy;

import java.io.Serializable;
import java.lang.reflect.Method;
import java.util.HashMap;
import java.util.Map;

public class CglibProxy implements MethodInterceptor, Serializable {
    private static final long serialVersionUID = 2951173584465042688L;
    private Object target;
    private transient Map<String, Object> changeMap = new HashMap<>();

    public CglibProxy() {
    }

    public Object getInstance(final Object target) {
        this.target = target;
        Enhancer enhancer = new Enhancer();
        enhancer.setSuperclass(this.target.getClass());
        enhancer.setCallback(this);
        return enhancer.create();
    }

    @Override
    public Object intercept(Object o, Method method, Object[] objects, MethodProxy methodProxy) throws Throwable {
        String methodName = method.getName();
        if (methodName.startsWith("set") && methodName.length() > 3) {
            Object value = objects[0];
            String fieldName = methodName.substring(3, 4).toLowerCase() + methodName.substring(4);
            changeMap.put(fieldName, value);
        }
        Object result = method.invoke(target, objects);
        return result;
    }

    public Object getTarget() {
        return target;
    }

    public Map<String, Object> getChangeMap() {
        return changeMap;
    }
}
